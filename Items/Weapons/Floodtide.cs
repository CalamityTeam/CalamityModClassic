using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
    public class Floodtide : ModItem
    {
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Floodtide");
			//Tooltip.SetDefault("Launches sharks, because sharks are awesome!");
		}

        public override void SetDefaults()
        {
            Item.damage = 52;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 23;
            Item.useAnimation = 23;
			Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5.5f;
            Item.value = 170000;
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.MiniSharkron;
            Item.shootSpeed = 11f;
        }
        
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(5))
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.FishronWings);
            }
        }
        
        public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "VictideBar", 5);
	        recipe.AddIngredient(ItemID.SharkFin, 5);
	        recipe.AddIngredient(ItemID.AdamantiteBar, 5);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	        recipe = CreateRecipe();
	        recipe.AddIngredient(null, "VictideBar", 5);
	        recipe.AddIngredient(ItemID.SharkFin, 5);
	        recipe.AddIngredient(ItemID.TitaniumBar, 5);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
    }
}