using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage
{
	public class BallisticPoisonBomb : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ballistic Poison Bomb");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 30;
			Item.damage = 55;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 22;
			Item.useStyle = 1;
			Item.useTime = 22;
			Item.knockBack = 6.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 38;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
			Item.shoot = Mod.Find<ModProjectile>("BallisticPoisonBomb").Type;
			Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DepthCells", 10);
            recipe.AddIngredient(null, "SulphurousSand", 20);
            recipe.AddIngredient(null, "Tenebris", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
