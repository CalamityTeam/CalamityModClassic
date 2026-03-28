using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class AnarchyBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Anarchy Blade");
			// Tooltip.SetDefault("The lower your life the more damage this blade does\nYour hits will generate a large explosion\nIf you're below 50% life your hits have a chance to instantly kill regular enemies");
		}

		public override void SetDefaults()
		{
			Item.width = 98;
			Item.damage = 110;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 19;
			Item.useTime = 19;
			Item.useTurn = true;
			Item.useStyle = 1;
			Item.knockBack = 7.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 98;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
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
	        if (Main.rand.Next(3) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 235);
	        }
	    }
	    
	    public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	    {
	    	int lifeAmount = player.statLifeMax2 - player.statLife;
	    	float damageAdd = (((float)lifeAmount * 0.1f) + (float)Item.damage);
	    	damage.Base = (int)(damageAdd * player.GetDamage(DamageClass.Melee).Base);
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("BrimstoneBoom").Type, (int)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative), Item.knockBack, Main.myPlayer);
	    	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);

	    	if (player.statLife < (player.statLifeMax2 * 0.5f) && Main.rand.Next(5) == 0 && !target.boss && target.type != 477 &&
				target.type != 327 && target.type != 135 && target.type != 136 && target.type != 325 && target.type != 344 && target.type != 346 && target.type != 345 &&
				target.type != Mod.Find<ModNPC>("Reaper").Type && target.type != Mod.Find<ModNPC>("Mauler").Type && target.type != Mod.Find<ModNPC>("EidolonWyrmHead").Type &&
				target.type != Mod.Find<ModNPC>("EidolonWyrmHeadHuge").Type && target.type != Mod.Find<ModNPC>("ColossalSquid").Type && target.type != NPCID.DD2Betsy)
	    	{
				if (!CalamityPlayerPreTrailer.areThereAnyDamnBosses)
				{
					target.life = 0;
					target.HitEffect(0, 10.0);
					target.active = false;
					target.NPCLoot();
				}
	    	}
		}
	}
}
