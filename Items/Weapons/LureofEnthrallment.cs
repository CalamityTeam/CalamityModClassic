using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class LureofEnthrallment : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/7.Leviathan/LureofEnthrallment");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Lure of Enthrallment");
        Item.damage = 50;
        Item.mana = 10;
        Item.width = 56;
        Item.height = 56;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = 1;
        ////Tooltip.SetDefault("Summons a siren lure to fight for you\nThe lure stays in place, shooting water spears, ice mist, and treble clefs at nearby enemies");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5.5f;
        Item.value = 750000;
        Item.rare = 7;
        Item.UseSound = SoundID.Item44;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("SirenLure").Type;
        Item.shootSpeed = 14f;
        Item.DamageType = DamageClass.Summon;
        Item.sentry = true;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
    	int i = Main.myPlayer;
		float num72 = Item.shootSpeed;
		int num73 = 0;
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
		Projectile.NewProjectile(source, vector2.X, vector2.Y, num78, num79, Mod.Find<ModProjectile>("SirenLure").Type, num73, num74, i, 0f, 0f);
		return false;
    }
}}