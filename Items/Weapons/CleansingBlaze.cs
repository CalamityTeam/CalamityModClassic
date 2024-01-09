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
	public class CleansingBlaze : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cleansing Blaze");
			//Tooltip.SetDefault("90% chance to not consume gel");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 242;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 60;
			Item.height = 34;
			Item.useTime = 3;
			Item.useAnimation = 12;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4f;
			Item.UseSound = SoundID.Item34;
			Item.value = 1350000;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("EssenceFire").Type;
			Item.shootSpeed = 14f;
			Item.useAmmo = 23;
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
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        int num6 = Main.rand.Next(2, 4);
	        for (int index = 0; index < num6; ++index)
	        {
	            float SpeedX = velocity.X + (float) Main.rand.Next(-15, 16) * 0.05f;
	            float SpeedY = velocity.Y + (float) Main.rand.Next(-15, 16) * 0.05f;
	            Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	}
	    	return false;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 90)
	    		return false;
	    	return true;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "CosmiliteBar", 12);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}