using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Malevolence : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/8.Plaguebringer/Malevolence");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Malevolence");
        Item.damage = 51;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 28;
        Item.height = 58;
        Item.useTime = 18;
        Item.useAnimation = 18;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        ////Tooltip.SetDefault("Fires two plague arrows at once");
        Item.knockBack = 3f;
        Item.value = 500000;
        Item.rare = 8;
        Item.UseSound = SoundID.Item97;
        Item.autoReuse = true;
        Item.shootSpeed = 12f;
        Item.shoot = Mod.Find<ModProjectile>("PlagueArrow").Type; //idk why but all the guns in the vanilla source have this
        Item.useAmmo = 40;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	for (int i = 0; i < 2; i++) // Will shoot 2 arrows
    	{
            float SpeedX = velocity.X + (float) Main.rand.Next(-20, 21) * 0.05f;
            float SpeedY = velocity.Y + (float) Main.rand.Next(-20, 21) * 0.05f;
        	Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), Mod.Find<ModProjectile>("PlagueArrow").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
    	}
    	return false;
	}
}}