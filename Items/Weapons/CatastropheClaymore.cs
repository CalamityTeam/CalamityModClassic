using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class CatastropheClaymore : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Catastrophe Claymore");
			// Tooltip.SetDefault("Remnant of a specular nova");
		}

		public override void SetDefaults()
		{
			Item.width = 56;
			Item.damage = 67;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 23;
			Item.useTime = 23;
			Item.useTurn = true;
			Item.useStyle = 1;
			Item.knockBack = 6.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 56;
            Item.value = Item.buyPrice(0, 48, 0, 0);
            Item.rare = 6;
			Item.shoot = Mod.Find<ModProjectile>("CalamityAura").Type;
			Item.shootSpeed = 11f;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	switch (Main.rand.Next(3))
			{
	    		case 0: type = Mod.Find<ModProjectile>("CalamityAura").Type; break;
	    		case 1: type = Mod.Find<ModProjectile>("CalamityAuraType2").Type; break;
	    		case 2: type = Mod.Find<ModProjectile>("CalamityAuraType3").Type; break;
	    		default: break;
			}
	       	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, Main.myPlayer);
	    	return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HallowedBar, 10);
			recipe.AddIngredient(ItemID.CrystalShard, 15);
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddIngredient(ItemID.CursedFlame, 5);
			recipe.AddIngredient(ItemID.SoulofMight, 3);
			recipe.AddIngredient(ItemID.SoulofSight, 3);
			recipe.AddIngredient(ItemID.SoulofFright, 3);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	        recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HallowedBar, 10);
			recipe.AddIngredient(ItemID.CrystalShard, 15);
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddIngredient(ItemID.Ichor, 5);
			recipe.AddIngredient(ItemID.SoulofMight, 3);
			recipe.AddIngredient(ItemID.SoulofSight, 3);
			recipe.AddIngredient(ItemID.SoulofFright, 3);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(3) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 73);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
	    	if(Main.rand.Next(3) == 0)
	    	{
	    		target.AddBuff(BuffID.CursedInferno, 200);
	    		target.AddBuff(BuffID.OnFire, 200);
	    		target.AddBuff(BuffID.Frostburn, 200);
	    	}
		}
	}
}
