using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class BloodyEdge : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bloody Edge");
			// Tooltip.SetDefault("Chance to heal the player on enemy hits");
		}

		public override void SetDefaults()
		{
			Item.width = 46;
			Item.damage = 47;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 23;
			Item.useStyle = 1;
			Item.useTime = 23;
			Item.knockBack = 5.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.useTurn = true;
			Item.height = 60;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
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
	        if (Main.rand.Next(5) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 5);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
	    	if (target.type == NPCID.TargetDummy || !target.canGhostHeal)
			{
				return;
			}
	    	int healAmount = (Main.rand.Next(3) + 1);
	    	if (Main.rand.Next(2) == 0)
	    	{
	    		player.statLife += healAmount;
	    		player.HealEffect(healAmount);
	    	}
		}
	}
}
