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
    public class IceStar : CalamityDamageItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Star");
            // Tooltip.SetDefault("Ice Stars are too brittle to be recovered after being thrown");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 62;
            Item.damage = 35;
            Item.noMelee = true;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 12;
            Item.crit = 7;
            Item.useStyle = 1;
            Item.useTime = 12;
            Item.knockBack = 2.5f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 62;
            Item.maxStack = 999;
            Item.value = 3000;
            Item.rare = 5;
            Item.shoot = Mod.Find<ModProjectile>("IceStarProjectile").Type;
            Item.shootSpeed = 14f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(null, "CryoBar");
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }
    }
}
