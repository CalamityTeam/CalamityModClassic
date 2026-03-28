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
    public class SeashellBoomerang : CalamityDamageItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Seashell Boomerang");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 18;
            Item.damage = 15;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 15;
            Item.useStyle = 1;
            Item.useTime = 15;
            Item.knockBack = 5.5f;
            Item.UseSound = SoundID.Item1;
            Item.height = 34;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 2;
            Item.shoot = Mod.Find<ModProjectile>("SeashellBoomerangProjectile").Type;
            Item.shootSpeed = 11.5f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "VictideBar", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
