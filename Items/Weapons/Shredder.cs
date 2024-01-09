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
	public class Shredder : ModItem
	{
	    public override void SetDefaults()
	    {
			Item.damage = 23;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 56;
			Item.height = 24;
			Item.useTime = 3;
			Item.reuseDelay = 10;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 1.5f;
			Item.value = 1000000;
			Item.UseSound = SoundID.Item31;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 12f;
			Item.useAmmo = 97;
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
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
    		{
				int num6 = Main.rand.Next(5, 12);
			    for (int index = 0; index < num6; ++index)
			    {
			        float num7 = velocity.X;
			        float num8 = velocity.Y;
			        float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
			        float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
			        Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("ChargedBlast3").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			    }
			    return false;
			}
			else
			{
				int num6 = Main.rand.Next(5, 12);
			    for (int index = 0; index < num6; ++index)
			    {
			        float num7 = velocity.X;
			        float num8 = velocity.Y;
			        float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
			        float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
			        Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("ChargedBlast").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			    }
			    return false;
			}
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "GalacticaSingularity", 5);
            recipe.AddIngredient(null, "BarofLife", 5);
            recipe.AddIngredient(null, "ChargedDartRifle");
            recipe.AddIngredient(ItemID.LunarBar, 5);
            recipe.AddIngredient(ItemID.ClockworkAssaultRifle);
            recipe.AddIngredient(ItemID.Shotgun);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
		}
	}
}