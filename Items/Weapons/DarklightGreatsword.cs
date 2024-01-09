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
	public class DarklightGreatsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Darklight Greatsword");
		}

		public override void SetDefaults()
		{
			Item.width = 44;
			Item.damage = 55;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 24;
			Item.useTurn = true;
			Item.knockBack = 5;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 50;
			Item.value = 305000;
			Item.rare = ItemRarityID.Pink;
			Item.shoot = Mod.Find<ModProjectile>("StarCrystal").Type;
			Item.shootSpeed = 16f;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	        Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage * 0.6f), knockback, player.whoAmI, 0.0f, 0.0f);
	        return false;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "VerstaltiteBar", 12);
			recipe.AddIngredient(ItemID.FallenStar, 5);
			recipe.AddIngredient(ItemID.SoulofNight);
			recipe.AddIngredient(ItemID.SoulofLight);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(3))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.WaterCandle);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(BuffID.Frostburn, 100);
		}
	}
}
