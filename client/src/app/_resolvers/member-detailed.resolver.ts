import { ResolveFn } from '@angular/router';
import { Member } from '../_models/member';
import { inject } from '@angular/core';
import { MembersService } from '../_services/members.service';

export const memberDetailedResolver: ResolveFn<Member> = (route, state) => {
  const memberService = inject(MembersService);
  var username = route.paramMap.get('username');
  if (!username) {
    username = 'Karen';
  }
  return memberService.getMember(username);
};
