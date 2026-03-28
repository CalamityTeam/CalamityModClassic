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
    public class TitaniumShuriken : CalamityDamageItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Titanium Shuriken");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 38;
            Item.damage = 31;
            Item.noMelee = true;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 9;
            Item.crit = 10;
            Item.useStyle = 1;
            Item.useTime = 9;
            Item.knockBack = 3f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 38;
            Item.maxStack = 999;
            Item.value = 2000;
            Item.rare = 4;
            Item.shoot = Mod.Find<ModProjectile>("TitaniumShurikenProjectile").Type;
            Item.shootSpeed = 16f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(30);
            recipe.AddIngredient(ItemID.TitaniumBar);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
