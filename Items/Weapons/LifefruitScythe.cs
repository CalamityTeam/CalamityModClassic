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
	public class LifefruitScythe : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Lifefruit Scythe");
			//Tooltip.SetDefault("Heals the player when attacking enemies");
		}

		public override void SetDefaults()
		{
			Item.width = 66;
			Item.damage = 215;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 18;
			Item.useTurn = true;
			Item.knockBack = 7.5f;
			Item.UseSound = SoundID.Item71;
			Item.autoReuse = true;
			Item.height = 68;
			Item.value = 1250000;
			Item.shoot = Mod.Find<ModProjectile>("LifeScythe").Type;
			Item.shootSpeed = 9f;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(0, 255, 200);
	            }
	        }
	    }
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "UeliaceBar", 15);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(4))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.CursedTorch);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	if (target.type == NPCID.TargetDummy)
			{
				return;
			}
	    	player.statLife += 3;
	    	player.HealEffect(3);
			target.AddBuff(BuffID.OnFire, 200);
			target.AddBuff(BuffID.CursedInferno, 200);
		}
	}
}
