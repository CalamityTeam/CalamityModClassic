using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Cryogen
{
    public class SnowstormStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Snowstorm Staff");
            // Tooltip.SetDefault("Fires a snowflake that follows the mouse cursor");
        }

        public override void SetDefaults()
        {
            Item.damage = 53;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.mana = 13;
            Item.width = 66;
            Item.height = 66;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
            Item.UseSound = SoundID.Item46;
            Item.shoot = Mod.Find<ModProjectile>("Snowflake").Type;
            Item.shootSpeed = 7f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "CryoBar", 6);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }
    }
}