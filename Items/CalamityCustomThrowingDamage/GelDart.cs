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
    public class GelDart : CalamityDamageItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gel Dart");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 14;
            Item.damage = 25;
            Item.noMelee = true;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 11;
            Item.useStyle = 1;
            Item.useTime = 11;
            Item.knockBack = 2.5f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 28;
            Item.maxStack = 999;
            Item.value = 1000;
            Item.rare = 4;
            Item.shoot = Mod.Find<ModProjectile>("GelDartProjectile").Type;
            Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(150);
            recipe.AddIngredient(null, "PurifiedGel", 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
