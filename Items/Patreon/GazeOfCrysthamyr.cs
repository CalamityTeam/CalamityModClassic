using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.Patreon
{
    class GazeOfCrysthamyr : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gaze of Crysthamyr");
            // Tooltip.SetDefault("Summons a shadow dragon");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 4;
			Item.rare = 10;
			Item.value = Item.buyPrice(3, 0, 0, 0);
            Item.UseSound = SoundID.NPCHit56;
            Item.noMelee = true;
            Item.mountType = Mod.Find<ModMount>("Crysthamyr").Type;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 21;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DD2PetDragon);
			recipe.AddIngredient(ItemID.SoulofNight, 100);
			recipe.AddIngredient(null, "DarksunFragment", 50);
			recipe.AddIngredient(null, "ExodiumClusterOre", 25);
			recipe.AddTile(null, "DraedonsForge");
			recipe.Register();
		}
	}
}
