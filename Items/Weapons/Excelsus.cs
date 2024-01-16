using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Excelsus : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/10.DevourerofGods/Excelsus");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Excelsus");
		Item.width = 70;  //The width of the .png file in pixels divided by 2.
		Item.damage = 280;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 20;
		Item.useStyle = 1;
		Item.useTime = 20;
		Item.useTurn = true;
		////Tooltip.SetDefault("Fires influx beams and summons laser fountains on enemy hits");
		Item.knockBack = 8f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 78;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 1250000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shoot = 451;
		Item.shootSpeed = 18f;
	}

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("LaserFountain").Type, 0, 0, Main.myPlayer);
	}
}}
