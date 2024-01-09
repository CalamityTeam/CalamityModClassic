using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class NeptunesBounty : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Neptune's Bounty");
		}

		public override void SetDefaults()
		{
			Item.width = 66;
			Item.damage = 380;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 9f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 72;
			Item.value = 5000000;
			Item.shoot = Mod.Find<ModProjectile>("NeptuneOrb").Type;
			Item.shootSpeed = 25f;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(43, 96, 222);
	            }
	        }
	    }
		
		public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "AbyssBlade");
	        recipe.AddIngredient(null, "CosmiliteBar", 5);
	        recipe.AddIngredient(null, "Phantoplasm", 5);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(3))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Water);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 600);
		}
	}
}
