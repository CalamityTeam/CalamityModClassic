using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons.SunkenSea
{
    public class Whirlpool : ModItem
    {
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Riptide");
            // Tooltip.SetDefault("Sprays a spiral of aqua streams in random directions");
        }
		
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.TheEyeOfCthulhu);
            Item.damage = 18;
            Item.width = 30;
			Item.height = 44;
			Item.value = Item.buyPrice(0, 2, 0, 0);
			Item.rare = 2;
            Item.knockBack = 1;
            Item.channel = true;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useStyle = 8;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.shoot = Mod.Find<ModProjectile>("WhirlpoolProjectile").Type;
            Item.shootSpeed = 18f;
            Item.UseSound = SoundID.Item1;
            ItemID.Sets.Yoyo[Item.type] = true;
        }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "SeaPrism", 7);
			recipe.AddIngredient(null, "Navystone", 10);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
    }
}
