using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class EldritchTome : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/4.SlimeGod/EldritchTome");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Eldritch Tome");
        Item.damage = 32;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 7;
        Item.width = 28;
        Item.crit = 3;
        Item.height = 30;
        Item.useTime = 7;
        Item.useAnimation = 21;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Casts eldritch tentacles to spear your enemies");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 3.5f;
        Item.value = 180000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item103;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("EldritchTentacle").Type;
        Item.shootSpeed = 12f;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	int i = Main.myPlayer;
		int num73 = Item.damage;
		float num74 = Item.knockBack;
    	num74 = player.GetWeaponKnockback(Item, num74);
    	player.itemTime = Item.useTime;
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
    	float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
		float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
    	Vector2 value2 = new Vector2(num78, num79);
		value2.Normalize();
		Vector2 value3 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
		value3.Normalize();
		value2 = value2 * 4f + value3;
		value2.Normalize();
		value2 *= Item.shootSpeed;
		float num91 = (float)Main.rand.Next(10, 80) * 0.001f;
		if (Main.rand.Next(2) == 0)
		{
			num91 *= -1f;
		}
		float num92 = (float)Main.rand.Next(10, 80) * 0.001f;
		if (Main.rand.Next(2) == 0)
		{
			num92 *= -1f;
		}
		Projectile.NewProjectile(source, vector2.X, vector2.Y, value2.X, value2.Y, Mod.Find<ModProjectile>("EldritchTentacle").Type, num73, num74, i, num92, num91);
    	return false;
	}
}}