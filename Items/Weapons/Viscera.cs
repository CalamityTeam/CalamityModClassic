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
    public class Viscera : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Viscera");
            // Tooltip.SetDefault("The more tiles and enemies the beam bounces off of or travels through the more healing the beam does");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 153;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 15;
            Item.width = 50;
            Item.height = 52;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 6f;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Viscera").Type;
            Item.shootSpeed = 6f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "BloodstoneCore", 4);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}