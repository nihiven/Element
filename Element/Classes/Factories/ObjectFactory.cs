using Element.Classes;
using Element.Interfaces;
using Element.Managers;

namespace Element
{
    /// <summary>
    /// Create new objects.
    /// </summary>
    public static class ObjectFactory
    {
        public static object New(string name)
        {
            switch (name)
            {
                case ComponentStrings.ActiveGear:
                    return ActiveGear();
                case ComponentStrings.ControllerDebug:
                    return ControllerDebug();
                case ComponentStrings.Debug:
                    return Debug();
                case ComponentStrings.EntityManager:
                    return EntityManager();
                case ComponentStrings.GameManager:
                    return Game();
                case ComponentStrings.GameOptions:
                    return GameOptions();
                case ComponentStrings.HUD:
                    return HUD();
                case ComponentStrings.Input:
                    return Input();
                case ComponentStrings.Inventory:
                    return Inventory();
                case ComponentStrings.ItemDebug:
                    return ItemDebug();
                case ComponentStrings.ItemManager:
                    return ItemManager();
                case ComponentStrings.Player:
                    return Player();
            }

            throw new System.ArgumentException("Parameter dose not exist in component string list", "name");
        }

        public static IActiveGear ActiveGear()
        {
            return new ActiveGear();
        }

        public static IControllerDebug ControllerDebug()
        {
            return new ControllerDebug(
                input: ObjectManager.Get<IInput>(ComponentStrings.Input),
                contentManager: ObjectManager.Get<IContentManager>(ComponentStrings.ContentManager),
                graphics: ObjectManager.Get<IGraphics>(ComponentStrings.Graphics)
            );
        }

        public static IDebug Debug()
        {
            return new EverDebug(
                contentManager: ObjectManager.Get<IContentManager>(ComponentStrings.ContentManager)
            );
        }

        public static IEntityManager EntityManager()
        {
            return new EntityManager();
        }

        public static IGameManager Game()
        {
            return new GameManager(
                gameOptions: ObjectManager.Get<IGameOptions>(ComponentStrings.GameOptions),
                debug: ObjectManager.Get<IDebug>(ComponentStrings.Debug),
                itemManager: ObjectManager.Get<IItemManager>(ComponentStrings.ItemManager),
                entityManager: ObjectManager.Get<IEntityManager>(ComponentStrings.EntityManager),
                hud: ObjectManager.Get<IHud>(ComponentStrings.HUD),
                player: ObjectManager.Get<IPlayer>(ComponentStrings.Player),
                inventory: ObjectManager.Get<IInventory>(ComponentStrings.Inventory),
                activeGear: ObjectManager.Get<IActiveGear>(ComponentStrings.ActiveGear)
            );
        }

        public static IGameOptions GameOptions()
        {
            return new GameOptions();
        }

        public static IHud HUD()
        {
            return new Hud(
                graphics: ObjectManager.Get<IGraphics>(ComponentStrings.Graphics),
                player: ObjectManager.Get<IPlayer>(ComponentStrings.Player),
                activeGear: ObjectManager.Get<IActiveGear>(ComponentStrings.ActiveGear),
                fontBig: ObjectManager.Get<IContentManager>(ComponentStrings.ContentManager).GetFont("ArialBig"),
                fontSmall: ObjectManager.Get<IContentManager>(ComponentStrings.ContentManager).GetFont("Arial")
            );
        }

        public static IInput Input()
        {
            return new XB1Pad();
        }

        public static IInventory Inventory()
        {
            return new Inventory(
                input: ObjectManager.Get<IInput>(ComponentStrings.Input),
                contentManager: ObjectManager.Get<IContentManager>(ComponentStrings.ContentManager),
                activeGear: ObjectManager.Get<IActiveGear>(ComponentStrings.ActiveGear)
            );
        }

        public static IUpdate ItemDebug()
        {
            return new ItemDebug(
                input: ObjectManager.Get<IInput>(ComponentStrings.Input),
                itemManager: ObjectManager.Get<IItemManager>(ComponentStrings.ItemManager)
            );
        }

        public static IItemManager ItemManager()
        {
            return new ItemManager();
        }

        public static IPlayer Player()
        {
            return new Player(
                input: ObjectManager.Get<IInput>(ComponentStrings.Input),
                animatedSprite: ObjectManager.Get<IContentManager>(ComponentStrings.ContentManager).GetAnimatedSprite("female"),
                activeGear: ObjectManager.Get<IActiveGear>(ComponentStrings.ActiveGear),
                itemManager: ObjectManager.Get<IItemManager>(ComponentStrings.ItemManager),
                inventory: ObjectManager.Get<IInventory>(ComponentStrings.Inventory)
            );
        }
    }
}
