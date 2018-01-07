using Element.Classes;
using Element.Interfaces;
using Element.Managers;

namespace Element
{
    public static class ObjectFactory
    {
        public static IInventory NewInventory(IPlayer owner)
        {
            return new Inventory(
                gameOptions: ObjectManager.Get<IGameOptions>("gameOptions"),
                input: ObjectManager.Get<IInput>("input"),
                contentManager: ObjectManager.Get<IContentManager>("contentManager"),
                itemManager: ObjectManager.Get<IItemManager>("itemManager"),
                owner: owner
            );
        }

        public static IHud NewHud()
        {
            return new Hud(
                graphics: ObjectManager.Get<IGraphics>("graphics"),
                player: ObjectManager.Get<IPlayer>("player"),
                contentManager: ObjectManager.Get<IContentManager>("contentManager")
                );
        }

        public static IGameOptions NewGameOptions()
        {
            return new GameOptions();
        }

        public static IPlayer NewPlayer()
        {
            return new Player(
                input: ObjectManager.Get<IInput>("input"),
                contentManager: ObjectManager.Get<IContentManager>("contentManager"),
                inventory: ObjectFactory.NewInventory()
            );
        }

        public static IItemManager NewItemManager()
        {
            return new ItemManager();
        }

        public static IInput NewInput()
        {
            return new XB1Pad();
        }

        public static IControllerDebug NewControllerDebug()
        {
            return new ControllerDebug(
                input: ObjectManager.Get<IInput>("input"),
                contentManager: ObjectManager.Get<IContentManager>("contentManager"),
                graphics: ObjectManager.Get<IGraphics>("graphics")
            );
        }

        public static IDebug NewDebug()
        {
            return new Debug(
                contentManager: ObjectManager.Get<IContentManager>("contentManager")
            );
        }

        public static IUpdate NewItemDebug()
        {
            return new ItemDebug(
                input: ObjectManager.Get<IInput>("input"),
                itemManager: ObjectManager.Get<IItemManager>("itemManager")
            );
        }

        public static IGameManager NewGameManager()
        {
            return new GameManager(
                gameOptions: ObjectManager.Get<IGameOptions>("gameOptions"),
                debug: ObjectManager.Get<IDebug>("debug"),
                player: ObjectManager.Get<IPlayer>("player"),
                itemManager: ObjectManager.Get<IItemManager>("itemManager"),
                hud: ObjectManager.Get<IHud>("hud")
            );
        }
    }
}
