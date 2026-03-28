using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureStatigel
{
	public class StaticRefiner : ModItem
	{
		public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Used for special crafting");
        }

		public override void SetDefaults()
        {
            Item.width = 26;
			Item.height = 26;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("StaticRefiner").Type;
		}
	}
}