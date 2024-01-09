using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Providence {
public class HolyCollider : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Holy Collider");
		//Tooltip.SetDefault("Striking enemies will cause them to explode into holy fire");
	}

	public override void SetDefaults()
	{
		Item.width = 68;  //The width of the .png file in pixels divided by 2.
		Item.damage = 285;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 22;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 22;
		Item.useTurn = true;
		Item.knockBack = 7.75f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 80;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 5000000;  //Value is calculated in copper coins.
		Item.shootSpeed = 10f;
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

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		float spread = 45f * 0.0174f;
		double startAngle = Math.Atan2(Item.shootSpeed, Item.shootSpeed) - spread / 2;
		double deltaAngle = spread / 8f;
		double offsetAngle;
		int i;
		for (i = 0; i < 4; i++) 
		{
			offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
			int projectile1 = Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("HolyFire2").Type, (int)((double)hit.Damage * 0.85f), hit.Knockback, Main.myPlayer);
			int projectile2 = Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("HolyFire2").Type, (int)((double)hit.Damage * 0.85f), hit.Knockback, Main.myPlayer);
			Main.projectile[projectile1].hostile = false;
			Main.projectile[projectile1].friendly = true;
			Main.projectile[projectile1].timeLeft = 60;
			Main.projectile[projectile2].hostile = false;
			Main.projectile[projectile2].friendly = true;
			Main.projectile[projectile2].timeLeft = 60;
		}
	}
}}
