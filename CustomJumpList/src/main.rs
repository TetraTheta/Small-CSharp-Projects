#![allow(non_upper_case_globals)]
#![allow(non_snake_case)]

use windows::core::{Interface, GUID, HSTRING, PROPVARIANT};
use windows::UI::StartScreen::JumpList;
use windows::Win32::Storage::EnhancedStorage::PKEY_Title;
use windows::Win32::System::Com::{CoCreateInstance, CoInitializeEx, CLSCTX_INPROC_SERVER, COINIT_MULTITHREADED};
use windows::Win32::UI::Shell::Common::{IObjectArray, IObjectCollection};
use windows::Win32::UI::Shell::PropertiesSystem::IPropertyStore;
use windows::Win32::UI::Shell::{DestinationList, EnumerableObjectCollection, ICustomDestinationList, IShellLinkW, ShellLink};

fn main() {
  #[allow(unused_variables)] let CLSID_CustomDestinationList: GUID = GUID::from(r"77f10cf0-3db5-4966-b520-b7c54fd35ed6");
  #[allow(unused_variables)] let CLSID_EnumerableObjectCollection: GUID = GUID::from(r"2d3468c1-36a7-43b6-ac24-d3f02fd9607a");

  // https://github.com/derceg/explorerplusplus/blob/master/Explorer%2B%2B/Helper/ShellHelper.cpp#L968
  // https://github.com/qermit/jumplists/blob/master/jumplists/jumplist.py

  let jump_list: JumpList = unsafe {
    match CoCreateInstance(&DestinationList, None, CLSCTX_INPROC_SERVER) {
      Ok(jump_list) => jump_list,
      Err(e) => {
        eprintln!("Failed to create jump list");
        std::process::exit(1);
      }
    }
  };
  jump_list.Task



  // Launch Explorer
  std::process::Command::new("explorer.exe").spawn().unwrap();
}

fn new_shell_link(exe_path: &str, arguments: &str, title: &str, icon_path: &str, icon_index: i32) -> IShellLinkW {
  let shell_link: IShellLinkW = unsafe { CoCreateInstance(&ShellLink, None, CLSCTX_INPROC_SERVER).unwrap() };
  unsafe { shell_link.SetPath(&HSTRING::from(exe_path)).unwrap() }
  if !arguments.is_empty() {
    unsafe { shell_link.SetArguments(&HSTRING::from(arguments)).unwrap(); }
  }
  unsafe { shell_link.SetIconLocation(&HSTRING::from(icon_path), icon_index).unwrap() }

  let prop_store: IPropertyStore = shell_link.cast().unwrap();
  let title_val: PROPVARIANT = title.into();
  unsafe { prop_store.SetValue(&PKEY_Title, &title_val).unwrap(); }
  unsafe { prop_store.Commit().unwrap(); }

  shell_link
}
