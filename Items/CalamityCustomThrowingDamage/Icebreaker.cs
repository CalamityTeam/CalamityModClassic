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
    public class Icebreaker : CalamityDamageItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Icebreaker");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 60;
            Item.damage = 57;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 14;
            Item.useStyle = 1;
            Item.useTime = 14;
            Item.knockBack = 6.75f;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.height = 60;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
            Item.shoot = Mod.Find<ModProjectile>("Icebreaker").Type;
            Item.shootSpeed = 16f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "CryoBar", 11);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }
    }
}
