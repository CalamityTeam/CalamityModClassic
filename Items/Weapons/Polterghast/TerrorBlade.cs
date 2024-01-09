using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons.Polterghast
{
	public class TerrorBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Terror Blade");
			//Tooltip.SetDefault("Fires a terror beam that bounces off tiles\nOn every bounce it emits an explosion");
		}

		public override void SetDefaults()
		{
			Item.width = 82;
			Item.damage = 220;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 18;
			Item.useTime = 18;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 72;
			Item.value = 1000000;
			Item.shoot = Mod.Find<ModProjectile>("TerrorBeam").Type;
			Item.shootSpeed = 20f;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(0, 255, 0);
	            }
	        }
	    }
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(3))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.RedTorch);
	        }
	    }
	}
}
