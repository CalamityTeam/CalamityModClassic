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
    public class TruePaladinsHammer : CalamityDamageItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Paladin's Hammer");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 14;
            Item.damage = 180;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useAnimation = 13;
            Item.useStyle = 1;
            Item.useTime = 13;
            Item.knockBack = 20f;
            Item.UseSound = SoundID.Item1;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
            Item.shoot = Mod.Find<ModProjectile>("OPHammer").Type;
            Item.shootSpeed = 14f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PaladinsHammer);
            recipe.AddIngredient(null, "CalamityDust", 5);
            recipe.AddIngredient(null, "CoreofChaos", 5);
            recipe.AddIngredient(null, "CruptixBar", 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
