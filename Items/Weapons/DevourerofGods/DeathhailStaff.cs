using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.DevourerofGods {
public class DeathhailStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Deathhail Staff");
		//Tooltip.SetDefault("Rain death upon your foes!");
		Item.staff[Item.type] = true;
	}

    public override void SetDefaults()
    {
        Item.damage = 190;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 18;
        Item.width = 62;
        Item.height = 62;
        Item.useTime = 6;
        Item.useAnimation = 12;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4;
        Item.value = 1250000;
        Item.UseSound = SoundID.Item12;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("NebulaShot").Type;
        Item.shootSpeed = 18f;
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
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
    	int i = Main.myPlayer;
		float num72 = Item.shootSpeed;
		int num73 = damage;
		float num74 = knockback;
    	num74 = player.GetWeaponKnockback(Item, num74);
    	player.itemTime = Item.useTime;
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
		Vector2 value = Vector2.UnitX.RotatedBy((double)player.fullRotation, default(Vector2));
		Vector2 vector3 = Main.MouseWorld - vector2;
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
		int num107 = 2;
		for (int num108 = 0; num108 < num107; num108++)
		{
			vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
			vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
			vector2.Y -= (float)(100 * num108);
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
			float speedX4 = num78 + (float)Main.rand.Next(-50, 51) * 0.02f;
			float speedY4 = num79 + (float)Main.rand.Next(-50, 51) * 0.02f;
			int projectile1 = Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, speedY4, Mod.Find<ModProjectile>("NebulaShot").Type, num73, num74, i, 0f, (float)Main.rand.Next(10));
			int projectile2 = Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, speedY4, Mod.Find<ModProjectile>("IceBeam").Type, num73, num74, i, 0f, (float)Main.rand.Next(5));
			Main.projectile[projectile1].tileCollide = false;
			Main.projectile[projectile1].DamageType = DamageClass.Magic;
			Main.projectile[projectile1].timeLeft = 200;
			Main.projectile[projectile2].tileCollide = false;
			Main.projectile[projectile2].DamageType = DamageClass.Magic;
			Main.projectile[projectile2].timeLeft = 200;
		}
		return false;
	}
}}