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
	public class SubsumingVortex : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Subsuming Vortex");
			//Tooltip.SetDefault("Fires 3 vortexes of elemental energy");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 330;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 30;
	        Item.width = 28;
	        Item.height = 30;
	        Item.UseSound = SoundID.Item84;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 5f;
	        Item.value = 100000000;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("Vortex").Type;
	        Item.shootSpeed = 9f;
	    }
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
	            }
	        }
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{    
		    int num6 = 3;
		    for (int index = 0; index < num6; ++index)
		    {
		        float SpeedX = velocity.X + (float) Main.rand.Next(-50, 51) * 0.05f;
		        float SpeedY = velocity.Y + (float) Main.rand.Next(-50, 51) * 0.05f;
		        float ai = (Main.rand.NextFloat() + 0.5f);
		        Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, ai);
		    }
		    return false;
		}
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "AuguroftheElements");
	        recipe.AddIngredient(null, "NuclearFury");
	        recipe.AddIngredient(null, "RelicofRuin");
	        recipe.AddIngredient(null, "TearsofHeaven");
	        recipe.AddIngredient(null, "NightmareFuel", 10);
        	recipe.AddIngredient(null, "EndothermicEnergy", 10);
	        recipe.AddIngredient(null, "CosmiliteBar", 10);
	        recipe.AddIngredient(null, "Phantoplasm", 50);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}