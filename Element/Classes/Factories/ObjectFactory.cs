using Element.Classes;
using Element.Interfaces;
using Element.Managers;

namespace Element
{
    public static class ObjectFactory
    {
        public static IInventory NewInventory()
        {
            return new Inventory(
                gameOptions: ObjectManager.Get<IGameOptions>(ComponentStrings.GameOptions),
                input: ObjectManager.Get<IInput>(ComponentStrings.Input),
                contentManager: ObjectManager.Get<IContentManager>(ComponentStrings.ContentManager),
                itemManager: ObjectManager.Get<IItemManager>(ComponentStrings.ItemManager),
                activeGear: ObjectManager.Get<IActiveGear>(ComponentStrings.ActiveGear)
            );
        }

        public static IHud NewHud()
        {
            return new Hud(
                graphics: ObjectManager.Get<IGraphics>(ComponentStrings.Graphics),
                player: ObjectManager.Get<IPlayer>(ComponentStrings.Player),
                contentManager: ObjectManager.Get<IContentManager>(ComponentStrings.ContentManager),
                activeGear: ObjectManager.Get<IActiveGear>(ComponentStrings.ActiveGear)
            );
        }

        public static IGameOptions NewGameOptions()
        {
            return new GameOptions();
        }

        public static IPlayer NewPlayer()
        {
            return new Player(
                input: ObjectManager.Get<IInput>(ComponentStrings.Input),
                contentManager: ObjectManager.Get<IContentManager>(ComponentStrings.ContentManager),
                activeGear: ObjectManager.Get<IActiveGear>(ComponentStrings.ActiveGear),
                itemManager: ObjectManager.Get<IItemManager>(ComponentStrings.ItemManager),
                inventory: ObjectManager.Get<IInventory>(ComponentStrings.Inventory)
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
                input: ObjectManager.Get<IInput>(ComponentStrings.Input),
                contentManager: ObjectManager.Get<IContentManager>(ComponentStrings.ContentManager),
                graphics: ObjectManager.Get<IGraphics>(ComponentStrings.Graphics)
            );
        }

        public static IDebug NewDebug()
        {
            return new Debug(
                contentManager: ObjectManager.Get<IContentManager>(ComponentStrings.ContentManager)
            );
        }

        public static IUpdate NewItemDebug()
        {
            return new ItemDebug(
                input: ObjectManager.Get<IInput>(ComponentStrings.Input),
                itemManager: ObjectManager.Get<IItemManager>(ComponentStrings.ItemManager)
            );
        }

        public static IGameManager NewGameManager()
        {
            return new GameManager(
                gameOptions: ObjectManager.Get<IGameOptions>(ComponentStrings.GameOptions),
                debug: ObjectManager.Get<IDebug>(ComponentStrings.Debug),
                itemManager: ObjectManager.Get<IItemManager>(ComponentStrings.ItemManager),
                hud: ObjectManager.Get<IHud>(ComponentStrings.HUD),
                player: ObjectManager.Get<IPlayer>(ComponentStrings.Player),
                inventory: ObjectManager.Get<IInventory>(ComponentStrings.Inventory),
                activeGear: ObjectManager.Get<IActiveGear>(ComponentStrings.ActiveGear)
            );
        }

        public static IActiveGear NewActiveGear()
        {
            return new ActiveGear();
        }
    }
}
