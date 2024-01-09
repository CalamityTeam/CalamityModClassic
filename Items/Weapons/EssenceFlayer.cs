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
	public class EssenceFlayer : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Essence Flayer");
			//Tooltip.SetDefault("Shoots an essence scythe that generates healing spirits on enemy kills");
		}

		public override void SetDefaults()
		{
			Item.width = 60;
			Item.damage = 300;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 19;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 19;
			Item.useTurn = true;
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item71;
			Item.autoReuse = true;
			Item.height = 56;
			Item.value = 1350000;
			Item.shoot = Mod.Find<ModProjectile>("EssenceScythe").Type;
			Item.shootSpeed = 21f;
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
			recipe.AddIngredient(null, "CosmiliteBar", 11);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(3))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.ShadowbeamStaff);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 500);
		}
	}
}
