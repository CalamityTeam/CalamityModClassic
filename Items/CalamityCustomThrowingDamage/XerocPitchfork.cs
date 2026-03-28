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
    public class XerocPitchfork : CalamityDamageItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Xeroc Pitchfork");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 48;
            Item.damage = 360;
            Item.noMelee = true;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 19;
            Item.useStyle = 1;
            Item.useTime = 19;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 48;
            Item.maxStack = 999;
            Item.value = 10000;
            Item.rare = 9;
            Item.shoot = Mod.Find<ModProjectile>("XerocPitchforkProjectile").Type;
            Item.shootSpeed = 16f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(20);
            recipe.AddIngredient(null, "MeldiateBar");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
