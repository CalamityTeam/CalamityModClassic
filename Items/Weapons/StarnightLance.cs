using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class StarnightLance : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Starnight Lance");
        }

        public override void SetDefaults()
        {
            Item.width = 72;
            Item.damage = 68;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 23;
            Item.useStyle = 5;
            Item.useTime = 23;
            Item.knockBack = 6;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.height = 72;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
            Item.shoot = Mod.Find<ModProjectile>("StarnightLanceProjectile").Type;
            Item.shootSpeed = 6f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "VerstaltiteBar", 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
