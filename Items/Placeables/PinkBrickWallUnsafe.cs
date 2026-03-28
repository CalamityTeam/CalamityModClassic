using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.Placeables
{
    public class PinkBrickWallUnsafe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 7;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createWall = WallID.PinkDungeonUnsafe;
        }
    }
}