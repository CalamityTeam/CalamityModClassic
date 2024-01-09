using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class AnarchyBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Anarchy Blade");
			//Tooltip.SetDefault("The lower your life the more damage this blade does\nYour hits will generate a large explosion\nIf you're below 50% life your hits have a chance to instantly kill regular enemies");
		}

		public override void SetDefaults()
		{
			Item.width = 92;
			Item.damage = 100;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 19;
			Item.useTime = 19;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 7.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 92;
			Item.value = 1000000;
			Item.rare = ItemRarityID.Yellow;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "UnholyCore", 5);
	        recipe.AddIngredient(null, "CoreofChaos", 3);
	        recipe.AddIngredient(ItemID.BrokenHeroSword);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(3))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.LifeDrain);
	        }
	    }
	    
	    public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	    {
	    	int lifeAmount = player.statLifeMax2 - player.statLife;
	    	float damageAdd = (((float)lifeAmount * 0.1f) + 100f);
	    	damage.Base = (int)(damageAdd * player.GetDamage(DamageClass.Melee).Base);
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("BrimstoneBoom").Type, (int)((double)hit.Damage * 0.5f), hit.Knockback, Main.myPlayer);
	    	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);
	    	if (player.statLife < (player.statLifeMax2 * 0.5f) && Main.rand.NextBool(5)&& !target.boss)
	    	{
	    		target.life = 0;
	            target.HitEffect(0, 10.0);
	            target.active = false;
	            target.NPCLoot();
	    	}
		}
	}
}
