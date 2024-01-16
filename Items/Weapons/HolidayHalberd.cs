using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class HolidayHalberd : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/HolidayHalberd");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Holiday Halberd");
		Item.width = 64;  //The width of the .png file in pixels divided by 2.
		Item.damage = 85;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 17;
		Item.useStyle = 1;
		Item.useTime = 17;
		Item.useTurn = true;
		////Tooltip.SetDefault("idk I'm miserable with names\n- The General");
		Item.knockBack = 7.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 66;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 800000;  //Value is calculated in copper coins.
		Item.rare = 8;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("RedBall").Type;
		Item.shootSpeed = 12f;
	}
	
	public override void MeleeEffects(Player player, Rectangle hitbox)
    {
		int dustType = 0;
		switch (Main.rand.Next(4))
		{
			case 1: dustType = 107; break;
			case 2: dustType = 90; break;
		}
        if (Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, dustType);
        }
    }
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	switch (Main.rand.Next(6))
		{
    		case 1: type = Mod.Find<ModProjectile>("RedBall").Type; break;
    		case 2: type = Mod.Find<ModProjectile>("GreenBall").Type; break;
    		default: break;
		}
       	Projectile.NewProjectile(source, position, velocity,type, damage, knockback, Main.myPlayer);
    	return false;
	}
}}
