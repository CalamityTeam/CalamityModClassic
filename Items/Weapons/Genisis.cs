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
	public class Genisis : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Genisis");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 90;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 9;
	        Item.width = 66;
	        Item.height = 28;
	        Item.useTime = 3;
	        Item.useAnimation = 3;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 1.5f;
	        Item.value = 10000000;
	        Item.UseSound = SoundID.Item33;
	        Item.autoReuse = true;
	        Item.shootSpeed = 6f;
	        Item.shoot = Mod.Find<ModProjectile>("BigBeamofDeath").Type;
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
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
	        int num6 = 3;
	        float SpeedX = velocity.X + (float) Main.rand.Next(-20, 21) * 0.05f;
		    float SpeedY = velocity.Y + (float) Main.rand.Next(-20, 21) * 0.05f;
	        for (int index = 0; index < num6; ++index)
	        {
	            int projectile = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, 440, (int)((double)damage * 0.75f), knockback, player.whoAmI, 0.0f, 0.0f);
	            Main.projectile[projectile].timeLeft = 120;
	        	Main.projectile[projectile].velocity.X *= 1.05f;
	        	Main.projectile[projectile].velocity.Y *= 1.05f;
	        }
	        return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.LaserMachinegun);
	        recipe.AddIngredient(ItemID.LunarBar, 5);
	        recipe.AddIngredient(null, "BarofLife", 5);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}