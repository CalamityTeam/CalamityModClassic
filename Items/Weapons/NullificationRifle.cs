using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class NullificationRifle : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/NullificationRifle");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Nullification Pistol");
        Item.damage = 115;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 64;
        Item.height = 30;
        ////Tooltip.SetDefault("Is it nullable or not?  Let's find out!\nFires a fast null bullet that distorts NPC stats\nUses your life as ammo");
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 7f;
        Item.value = 1250000;
        Item.rare = 9;
        Item.UseSound = new Terraria.Audio.SoundStyle("CalamityModClassic1Point1/Sounds/Item/PlasmaBlast");
        Item.autoReuse = true;
        Item.shootSpeed = 25f;
        Item.shoot = Mod.Find<ModProjectile>("NullShot").Type;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	player.statLife -= 5;
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("NullShot").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}
}}