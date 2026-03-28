using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class ScourgeoftheCosmos : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scourge of the Cosmos");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.damage = 1500;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 20;
			Item.useStyle = 5;
			Item.useTime = 20;
			Item.knockBack = 5f;
			Item.UseSound = SoundID.Item109;
			Item.autoReuse = true;
			Item.height = 20;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("ScourgeoftheCosmos").Type;
			Item.shootSpeed = 15f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ScourgeoftheCorruptor);
            recipe.AddIngredient(null, "CosmiliteBar", 10);
            recipe.AddIngredient(null, "DarksunFragment", 10);
            recipe.AddIngredient(null, "XerocPitchfork", 200);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}
