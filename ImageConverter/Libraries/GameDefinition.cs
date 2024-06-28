namespace ImageConverter.Libraries {

  public enum Game { TOF, WW }

  public enum Operation { Background, Center, Foreground0, Foreground1, Foreground2, Foreground3, Foreground4, Full }

  public enum CropPosition { Bottom, Center, Full }

  public class GameDefinition {
    public string UID_AREA { get; }
    public string UID_POS { get; }
    public CropPosition CROP_POS { get; }
    public ushort CROP_HEIGHT { get; }

    public GameDefinition(Game game, Operation operation) {
      switch (game) {
        case Game.TOF:
          UID_AREA = "144:22";
          UID_POS = "1744:1059";
          switch (operation) {
            case Operation.Background:
              CROP_POS = CropPosition.Bottom;
              CROP_HEIGHT = 330;
              break;

            case Operation.Center:
              CROP_POS = CropPosition.Center;
              CROP_HEIGHT = 200;
              break;

            case Operation.Foreground0:
              CROP_POS = CropPosition.Bottom;
              CROP_HEIGHT = 280;
              break;

            case Operation.Foreground1:
              CROP_POS = CropPosition.Bottom;
              CROP_HEIGHT = 420;
              break;

            case Operation.Foreground2:
              CROP_POS = CropPosition.Bottom;
              CROP_HEIGHT = 495;
              break;

            case Operation.Foreground3:
              CROP_POS = CropPosition.Bottom;
              CROP_HEIGHT = 570;
              break;

            case Operation.Foreground4:
              CROP_POS = CropPosition.Bottom;
              CROP_HEIGHT = 645;
              break;

            case Operation.Full:
              CROP_POS = CropPosition.Full;
              CROP_HEIGHT = 0;
              break;
          }
          break;

        case Game.WW:
          UID_AREA = "144:22";
          UID_POS = "1744:1059";
          switch (operation) {
            case Operation.Background:
              CROP_POS = CropPosition.Bottom;
              CROP_HEIGHT = 330;
              break;

            case Operation.Center:
              CROP_POS = CropPosition.Center;
              CROP_HEIGHT = 200;
              break;

            case Operation.Foreground0:
              CROP_POS = CropPosition.Bottom;
              CROP_HEIGHT = 280;
              break;

            case Operation.Foreground1:
              CROP_POS = CropPosition.Bottom;
              CROP_HEIGHT = 420;
              break;

            case Operation.Foreground2:
              CROP_POS = CropPosition.Bottom;
              CROP_HEIGHT = 495;
              break;

            case Operation.Foreground3:
              CROP_POS = CropPosition.Bottom;
              CROP_HEIGHT = 570;
              break;

            case Operation.Foreground4:
              CROP_POS = CropPosition.Bottom;
              CROP_HEIGHT = 645;
              break;

            case Operation.Full:
              CROP_POS = CropPosition.Full;
              CROP_HEIGHT = 0;
              break;
          }
          break;
      }
    }
  }
}
