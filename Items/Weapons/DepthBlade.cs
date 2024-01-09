using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class DepthBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Depth Blade");
			//Tooltip.SetDefault("Hitting enemies will cause the crush depth debuff\nThe lower the enemies' defense the more damage they take from this debuff");
		}

		public override void SetDefaults()
		{
			Item.width = 42;
			Item.damage = 22;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 22;
			Item.useTime = 22;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 48;
			Item.value = 90000;
			Item.rare = ItemRarityID.Green;
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
	    	target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 180);
		}
	}
}
