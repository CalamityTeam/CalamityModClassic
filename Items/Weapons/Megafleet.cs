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
	public class Megafleet : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Megafleet");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 680;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 96;
			Item.height = 38;
			Item.useTime = 7;
			Item.useAnimation = 7;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 15f;
			Item.value = 10000000;
			Item.UseSound = SoundID.Item92;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("PlasmaBlast").Type;
			Item.shootSpeed = 15f;
			Item.useAmmo = 75;
		}
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(255, 0, 255);
	            }
	        }
	    }
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 95)
	    		return false;
	    	return true;
	    }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float SpeedX = velocity.X + (float) Main.rand.Next(-15, 16) * 0.05f;
		    float SpeedY = velocity.Y + (float) Main.rand.Next(-15, 16) * 0.05f;
		    Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Megalodon");
            recipe.AddIngredient(null, "Starfleet");
            recipe.AddIngredient(null, "ShadowspecBar", 5);
            recipe.AddIngredient(ItemID.SoulofMight, 30);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
		}
	}
}