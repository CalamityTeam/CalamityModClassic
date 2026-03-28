using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class CausticEdge : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Caustic Edge");
			// Tooltip.SetDefault("Inflicts poison and venom on enemy hits");
		}

		public override void SetDefaults()
		{
			Item.width = 46;
			Item.damage = 44;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useTurn = true;
			Item.useAnimation = 27;
			Item.useStyle = 1;
			Item.useTime = 27;
			Item.knockBack = 5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 48;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 2;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BladeofGrass);
			recipe.AddIngredient(ItemID.LavaBucket);
			recipe.AddIngredient(ItemID.Deathweed, 5);
	        recipe.AddTile(TileID.DemonAltar);	
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(5) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 74);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.Poisoned, 480);
			target.AddBuff(BuffID.Venom, 240);
		}
	}
}
