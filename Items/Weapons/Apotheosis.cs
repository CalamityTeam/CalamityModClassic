using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class Apotheosis : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Apotheosis");
			/* Tooltip.SetDefault("Unleashes interdimensional projection magic\n" +
                "Eat worms"); */
		}

		public override void SetDefaults()
		{
			Item.damage = 333;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 42;
			Item.width = 16;
			Item.height = 16;
			Item.useTime = 42;
			Item.useAnimation = 42;
			Item.useStyle = 5;
			Item.useTurn = false;
			Item.noMelee = true;
			Item.knockBack = 4f;
            Item.value = Item.buyPrice(5, 0, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item92;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("ApothMark").Type;
            Item.shootSpeed = 15;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 16;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SubsumingVortex");
            recipe.AddIngredient(null, "CosmicDischarge");
            recipe.AddIngredient(null, "StaffoftheMechworm", 3);
            recipe.AddIngredient(null, "Excelsus", 2);
            recipe.AddIngredient(null, "DarksunFragment", 77);
            recipe.AddIngredient(null, "NightmareFuel", 77);
            recipe.AddIngredient(null, "EndothermicEnergy", 77);
            recipe.AddIngredient(null, "CosmiliteBar", 77);
            recipe.AddIngredient(null, "Phantoplasm", 77);
            recipe.AddIngredient(null, "ShadowspecBar", 5);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
	}
}
