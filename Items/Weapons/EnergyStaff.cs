using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class EnergyStaff : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/EnergyStaff");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Energy Staff");
        Item.damage = 118;
        Item.DamageType = DamageClass.Summon;
        Item.sentry = true;
        Item.mana = 25;
        Item.width = 48;
        Item.height = 48;
        Item.useTime = 38;
        Item.useAnimation = 38;
        Item.useStyle = 5;
        Item.staff[Item.type] = true;
        ////Tooltip.SetDefault("Summons profaned energy near the mouse cursor that stays in place blasting enemies that draw too close");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5f;
        Item.value = 500000;
        Item.rare = 10;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("ProfanedEnergy").Type;
        Item.shootSpeed = 9f;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
    	int i = Main.myPlayer;
		float num72 = Item.shootSpeed;
		int num73 = Item.damage;
		float num74 = Item.knockBack;
    	num74 = player.GetWeaponKnockback(Item, num74);
    	player.itemTime = Item.useTime;
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
    	num78 = 0f;
		num79 = 0f;
		vector2.X = (float)Main.mouseX + Main.screenPosition.X;
		vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
		Projectile.NewProjectile(source, vector2.X, vector2.Y, num78, num79, Mod.Find<ModProjectile>("ProfanedEnergy").Type, num73, num74, i, 0f, 0f);
		return false;
    }
}}