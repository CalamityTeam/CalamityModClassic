using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class NeptunesBounty : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Neptune's Bounty");
            // Tooltip.SetDefault("Hitting enemies will cause the crush depth debuff\nThe lower the enemies' defense the more damage they take from this debuff");
        }

		public override void SetDefaults()
		{
			Item.width = 80;
			Item.damage = 540;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useTurn = true;
			Item.useStyle = 1;
			Item.knockBack = 9f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 80;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("NeptuneOrb").Type;
			Item.shootSpeed = 25f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}
		
		public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "AbyssBlade");
	        recipe.AddIngredient(null, "CosmiliteBar", 5);
	        recipe.AddIngredient(null, "Phantoplasm", 5);
            recipe.AddIngredient(null, "DepthCells", 15);
            recipe.AddIngredient(null, "Lumenite", 15);
            recipe.AddIngredient(null, "Tenebris", 5);
            recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(3) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 33);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 600);
		}
	}
}
