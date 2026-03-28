using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class AquaticDissolution : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aquatic Dissolution");
			// Tooltip.SetDefault("Fires aquatic jets from the sky that bounce off tiles");
		}

		public override void SetDefaults()
		{
			Item.width = 50;
			Item.damage = 165;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 16;
			Item.useTime = 16;
			Item.useTurn = true;
			Item.useStyle = 1;
			Item.knockBack = 5.5f;
			Item.UseSound = SoundID.Item60;
			Item.autoReuse = true;
			Item.height = 72;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("OceanBeam").Type;
			Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            for (int x = 0; x < 3; x++)
            {
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-100, 100), player.position.Y - 600f, 0f, 8f, type, damage, knockback, Main.myPlayer, 0f, 0f);
            }
            return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Mariana");
			recipe.AddIngredient(null, "UeliaceBar", 7);
			recipe.AddIngredient(null, "BarofLife", 2);
            recipe.AddIngredient(null, "Lumenite", 20);
            recipe.AddIngredient(null, "Tenebris", 5);
            recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(5) == 0)
			{
				int num250 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 33, (float)(player.direction * 2), 0f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.3f);
				Main.dust[num250].velocity *= 0.2f;
				Main.dust[num250].noGravity = true;
			}
	    }
	}
}
