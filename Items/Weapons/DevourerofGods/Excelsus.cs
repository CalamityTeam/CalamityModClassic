using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.DevourerofGods {
public class Excelsus : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Excelsus");
		//Tooltip.SetDefault("Fires influx beams and summons laser fountains on enemy hits");
	}

	public override void SetDefaults()
	{
		Item.width = 70;  //The width of the .png file in pixels divided by 2.
		Item.damage = 350;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 15;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 15;
		Item.useTurn = true;
		Item.knockBack = 8f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 82;  //The height of the .png file in pixels divided by 2.
		Item.value = 1250000;  //Value is calculated in copper coins.
		Item.shoot = ProjectileID.InfluxWaver;
		Item.shootSpeed = 18f;
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	int num6 = Main.rand.Next(5, 9);
	    for (int index = 0; index < num6; ++index)
	    {
	        float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
	        float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
	        Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    }
    	return false;
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

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("LaserFountain").Type, 0, 0, Main.myPlayer);
	}
}}
