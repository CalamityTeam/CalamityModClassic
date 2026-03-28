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
    public class TarragonThrowingDart : CalamityDamageItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tarragon Throwing Dart");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 34;
            Item.damage = 380;
            Item.noMelee = true;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 11;
            Item.useStyle = 1;
            Item.useTime = 11;
            Item.knockBack = 4.5f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 34;
            Item.maxStack = 999;
            Item.value = 25000;
            Item.shoot = Mod.Find<ModProjectile>("TarragonThrowingDartProjectile").Type;
            Item.shootSpeed = 24f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(null, "UeliaceBar");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
