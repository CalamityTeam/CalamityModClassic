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
	public class Effervescence : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Effervescence");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 110;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 17;
	        Item.width = 56;
	        Item.height = 26;
	        Item.useTime = 12;
	        Item.useAnimation = 12;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 3.75f;
	        Item.value = 1000000;
	        Item.UseSound = SoundID.Item95;
	        Item.autoReuse = true;
	        Item.shootSpeed = 13f;
	        Item.shoot = Mod.Find<ModProjectile>("UberBubble").Type;
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
			for (int randomBullets = 0; randomBullets <= 4; randomBullets++)
			{
				float SpeedX = velocity.X + (float) Main.rand.Next(-25, 26) * 0.05f;
	            float SpeedY = velocity.Y + (float) Main.rand.Next(-25, 26) * 0.05f;
				Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0f, 0f);
			}
	    	return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.BubbleGun);
	        recipe.AddIngredient(ItemID.Xenopopper);
	        recipe.AddIngredient(ItemID.LunarBar, 5);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}