using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class MirrorBlade : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/MirrorBlade");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Mirror Blade");
		Item.width = 50;  //The width of the .png file in pixels divided by 2.
		Item.damage = 190;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.mana = 5;
		Item.useAnimation = 18;
		Item.useTime = 18;  //Ranges from 1 to 55. 
		Item.useTurn = true;
		////Tooltip.SetDefault("The amount of contact damage an enemy does is added to this weapons' damage\nYou must hit an enemy with the blade to trigger this effect\nConsumes mana to fire mirror blasts");
		Item.useStyle = 1;
		Item.knockBack = 8f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 56;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 2000000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shootSpeed = 20f;
        Item.shoot = Mod.Find<ModProjectile>("MirrorBlast").Type;
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(3) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 234);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	int conDamage = target.damage + 190;
    	if (conDamage <= 190)
    	{
    		conDamage = 190;
    	}
    	Item.damage = conDamage;
	}
}}
