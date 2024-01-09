using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class BloodyEdge : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Bloody Edge");
			//Tooltip.SetDefault("Chance to heal the player on enemy hits");
		}

		public override void SetDefaults()
		{
			Item.width = 40;
			Item.damage = 43;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 23;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 23;
			Item.knockBack = 5.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.height = 42;
			Item.value = 160000;
			Item.rare = ItemRarityID.Orange;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LightsBane);
			recipe.AddIngredient(ItemID.BladeofGrass);
			recipe.AddIngredient(ItemID.Muramasa);
			recipe.AddIngredient(ItemID.FieryGreatsword);
	        recipe.AddTile(TileID.DemonAltar);	
	        recipe.Register();
	        recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BloodButcherer);
			recipe.AddIngredient(ItemID.BladeofGrass);
			recipe.AddIngredient(ItemID.Muramasa);
			recipe.AddIngredient(ItemID.FieryGreatsword);
	        recipe.AddTile(TileID.DemonAltar);	
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(5))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
	    	if (target.type == NPCID.TargetDummy)
			{
				return;
			}
	    	int healAmount = (Main.rand.Next(3) + 1);
	    	if (Main.rand.NextBool(2))
	    	{
	    		player.statLife += healAmount;
	    		player.HealEffect(healAmount);
	    	}
		}
	}
}
