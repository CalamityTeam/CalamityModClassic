using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class AquaticDischarge : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/1.DesertScourge/AquaticDischarge");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Aquatic Discharge");
		Item.useStyle = 3;
		Item.useTurn = false;
		Item.useAnimation = 15;
		Item.useTime = 15;  //Ranges from 1 to 55.
		Item.width = 32;  //The width of the .png file in pixels divided by 2.
		Item.height = 32;  //The height of the .png file in pixels divided by 2.
		Item.damage = 20;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.knockBack = 5.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.useTurn = true;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.maxStack = 1;
		////Tooltip.SetDefault("Enemies release electric sparks on death");
		Item.value = 8000;  //Value is calculated in copper coins.
		Item.rare = 2;  //Ranges from 1 to 11.
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 226);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	if (target.life <= 0)
    	{
    		Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("Spark").Type, hit.Damage, hit.Knockback, Main.myPlayer);
    	}
	}
}}
