using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class ArterialAssault : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Arterial Assault");
		//Tooltip.SetDefault("Fires a chain of bloodfire arrows from the sky");
	}

    public override void SetDefaults()
    {
        Item.damage = 170;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 38;
        Item.height = 86;
        Item.useTime = 3;
        Item.reuseDelay = 10;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4.25f;
        Item.value = 1000000;
        Item.UseSound = SoundID.Item102;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("BloodfireArrow").Type;
		Item.shootSpeed = 22f;
		Item.useAmmo = 40;
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
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "BloodstoneCore", 5);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	float num72 = Item.shootSpeed;
	    Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
	   	float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
		float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
		if (player.gravDir == -1f)
		{
			num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
		}
		float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
		float num81 = num80;
		if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
		{
			num78 = (float)player.direction;
			num79 = 0f;
			num80 = num72;
		}
		else
		{
			num80 = num72 / num80;
		}
	   	num78 *= num80;
		num79 *= num80;
		vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (-(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
		vector2.X = (vector2.X + player.Center.X) / 2f;
		vector2.Y -= 100f;
		num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
		num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
		if (num79 < 0f)
		{
			num79 *= -1f;
		}
		if (num79 < 20f)
		{
			num79 = 20f;
		}
		num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
		num80 = num72 / num80;
		num78 *= num80;
		num79 *= num80;
		float speedX4 = num78;
		float speedY4 = num79;
		int bloodfire = Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, speedY4, Mod.Find<ModProjectile>("BloodfireArrow").Type, damage, knockback, player.whoAmI, 0f, (float)Main.rand.Next(15));
	    Main.projectile[bloodfire].tileCollide = false;
    	return false;
	}
}}