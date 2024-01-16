using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class AncientShiv : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/AncientShiv");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Ancient Shiv");
		Item.useStyle = 3;
		Item.useTurn = false;
		Item.useAnimation = 15;
		Item.useTime = 15;  //Ranges from 1 to 55.
		Item.width = 30;  //The width of the .png file in pixels divided by 2.
		Item.height = 30;  //The height of the .png file in pixels divided by 2.
		Item.damage = 30;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.knockBack = 5.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.useTurn = true;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.maxStack = 1;
		////Tooltip.SetDefault("Enemies release a blue aura cloud on death");
		Item.value = 55000;  //Value is calculated in copper coins.
		Item.rare = 3;  //Ranges from 1 to 11.
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 15);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	if (target.life <= 0)
    	{
    		Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("BlueAura").Type, hit.Damage, hit.Knockback, Main.myPlayer);
    	}
	}
}}
